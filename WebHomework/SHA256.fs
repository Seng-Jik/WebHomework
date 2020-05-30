module SHA256

let sha256 (s:string) =
    use x = System.Security.Cryptography.SHA256Managed.Create()
    x.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))
    |> fun x -> System.BitConverter.ToString(x).Replace("-", "")
