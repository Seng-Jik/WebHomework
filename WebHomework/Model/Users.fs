module Model.Users

type User = {
    name : string
    password : string
}

let private userTable = Model.Connection.Database.SUsers

let register user =
    userTable.Create([
        "username",user.name :> obj
        "passwords",user.password :> obj
    ]) |> ignore
    Model.Connection.SqlContext.SubmitUpdates()

exception LoginFailed
type UserID = int
let login user : Result<UserID,exn> =
    query {
        for users in userTable do
        where (users.Username = user.name && users.Passwords = user.password)
        select users.Id
    }
    |> Seq.cast
    |> Seq.tryHead
    |> function
    | Some x -> Ok x
    | None -> Error LoginFailed

let (|UserInformation|_|) (x:Suave.Http.HttpContext) =
    match Suave.State.CookieStateStore.HttpContext.state x with
    | None -> None
    | Some session ->
        match session.get<int> "uid" with
        | None -> None
        | Some uid ->
            query {
                for users in userTable do
                where (uid = users.Id)
            }
            |> Seq.tryHead
            |> Option.map (fun x -> {
                name = x.Username
                password = x.Passwords
            })
