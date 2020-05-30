module Pages.Login

open FsHtml
open Suave
open Suave.Operators
open Components.Bootstrap
open Components.Common
open Suave.State.CookieStateStore

let login = 
    Pages.MasterPage.master None [
        bsCotainer [
            row [
                div [
                    "class" %= "col-4"
                    Text " "
                ]
                div [
                    "class" %= "col-4 col"
                    elem "center" [
                        h1 %"登录"

                        elem "form" [
                            "action" %= "/login"
                            "method" %= "post"

                            div [
                                row [
                                    "class" %= "input-group"
                                    elem "input" [
                                        "type" %= "text"
                                        "class" %= "form-control"
                                        "style" %= "margin-bottom:10px"
                                        "name" %= "username"
                                        "placeholder" %= "用户名"
                                    ]
                                ]

                                row [
                                    elem "input" [
                                        "type" %= "password"
                                        "class" %= "form-control"
                                        "style" %= "margin-bottom:10px"
                                        "name" %= "password"
                                        "placeholder" %= "密码"
                                    ]
                                ]

                                row [
                                    elem "button" [
                                        "type" %= "submit"
                                        "class" %= "btn btn-dark col-5"
                                        Text "登录"
                                    ]

                                    div [
                                        "class" %= "col-2"
                                        Text " "
                                    ]

                                    a [
                                        "class" %= "btn btn-dark col-5"
                                        "href" %= "register"
                                        Text "注册"
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
                div [
                    "class" %= "col-4"
                    Text " "
                ]
            ]
        ]
    ]

let setSessionValue (key : string) (value : 'T) : WebPart =
  context (fun ctx ->
    match HttpContext.state ctx with
    | Some state ->
        state.set key value
    | _ ->
        never // fail
    )

open Model.Users
open Suave.Http
open Suave.State.CookieStateStore
let loginPost (form:HttpRequest) context =
    async {
        let loginResult =
            login {
                name = form.["username"].Value
                password = SHA256.sha256 form.["password"].Value
            }
            |> function
            | Error LoginFailed -> 
                fsPage (ErrorPage.errorPage "登陆失败！" "/") context
            | Error e ->
                fsPage (ErrorPage.errorPage e.Message "/") context
            | Ok userID -> 
                (setSessionValue "uid" userID
                >=> Suave.Redirection.redirect "/newslist") context
        return! loginResult
    }

let exitPost =
    setSessionValue "uid" -1 >=> Suave.Redirection.redirect "/"
