namespace Api.Security;

// List of app role names.
// A common naming convention is resource.operation.constraint.
public static class Role
{
    public const string ProductReadAll = "Product.Read.All";
    public const string ProductReadWriteAll = "Product.ReadWrite.All";
}
