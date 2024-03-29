using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

namespace KindeAuthentication;

public static class AuthorizationPermissionsPolicyBuilder
{
    public static AuthorizationBuilder AddKindePermissionPolicies<T>(this AuthorizationBuilder builder) where T: class
    {
        var policyFields = GetPolicyFields<T>();

        foreach (var field in policyFields)
        {
            AddPolicy(builder, field);
        }

        return builder;
    }

    public static AuthorizationBuilder AddKindePermissionPolicies(this AuthorizationBuilder builder, List<string> permissions)
    {
        foreach (var permission in permissions)
        {
            AddPolicy(builder, permission);
        }

        return builder;
    }

    
    private static IEnumerable<FieldInfo> GetPolicyFields<T>() where T: class
    {
        var policyType = typeof(T);
        return policyType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(x => x.FieldType == typeof(string));
    }

    private static void AddPolicy(AuthorizationBuilder builder, FieldInfo field)
    {
        var permissionName = (string)field.GetValue(null)!;
        AddPolicy(builder, permissionName);
    }
    
    private static void AddPolicy(AuthorizationBuilder builder, string policyName)
    {
        builder.AddPolicy(policyName, policy => policy
            .RequireClaim(KindeClaimTypes.Permissions, policyName));
    }
}