open System
open System.Threading
open System.Net
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Diagnostics
open Pages.Components.Common
open Suave.State.CookieStateStore

let home = 
    System.Environment.CurrentDirectory.TrimEnd('/','\\').Replace('/','\\')
    |> fun x -> x.[..(-1)+(x.LastIndexOf '\\')]
    |> fun x -> x.[..(-1)+(x.LastIndexOf '\\')]
    |> fun x -> x + "\\Web\\"

let conf = {
    defaultConfig with
        bindings = [ HttpBinding.createSimple HTTP "0.0.0.0" 80 ] 
        homeFolder = Some home
}

let requestUserPage page _ (context:HttpContext) =
    async {
        match context with
        | Model.Users.UserInformation user ->
            return! fsPage (page user) context
        | _ -> 
            return! Suave.Redirection.redirect "/exit" context
    }
    


let routing = 
    statefulForSession
    >=> choose [
        GET >=> choose [
            path "/" >=> fsPage Pages.Login.login
            path "/login" >=> fsPage Pages.Login.login
            path "/register" >=> fsPage Pages.Register.registerPage
            path "/newslist" >=> request (requestUserPage Pages.NewsList.newslist)
            path "/newseditor" >=> request (requestUserPage Pages.NewsEditor.newsEditor)
            path "/newsviewer" >=> request Pages.NewsViewer.requestViewer
            path "/exit" >=> Pages.Login.exitPost
        ]

        POST >=> choose [
            path "/newseditor" >=> request Pages.NewsEditor.postNews
            path "/register" >=> request Pages.Register.registerPost
            path "/login" >=> request Pages.Login.loginPost
            path "/exit" >=> Pages.Login.exitPost
        ]
    ]

printfn "Web根目录为:%s" home

let listening, server = startWebServerAsync conf routing

printfn "已经启动在 localhost:80 的HTTP服务。"
//Process.Start ("http://localhost:80") |> ignore

Async.RunSynchronously server
