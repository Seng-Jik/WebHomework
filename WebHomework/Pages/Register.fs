module Pages.Register

open FsHtml
open Components.Bootstrap
open Components.Common
open Suave.Http
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Diagnostics

let registerPage =
    Pages.MasterPage.master None [
        bsCotainer [
            row [
                div [
                    "class" %= "col-4"
                    Text " "
                ]

                div [
                    "class" %= "col-4"
                    elem "center" [
                        h1 %"注册"

                        elem "form" [
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
                                    elem "input" [
                                        "type" %= "password"
                                        "class" %= "form-control"
                                        "style" %= "margin-bottom:10px"
                                        "name" %= "passwordAgain"
                                        "placeholder" %= "再输入一次密码"
                                    ]
                                ]

                                row [
                                    elem "button" [
                                        "type" %= "submit"
                                        "class" %= "btn btn-dark col-5"
                                        Text "注册"
                                    ]

                                    div [
                                        "class" %= "col-2"
                                        Text " "
                                    ]

                                    a [
                                        "class" %= "btn btn-dark col-5"
                                        "href" %= "/"
                                        Text "返回"
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
    
open Pages.ErrorPage
open Model.Users
let registerPost (form:HttpRequest) context =
    async {
        let userName = form.["username"].Value
        let password = form.["password"].Value
        let password2 = form.["passwordAgain"].Value
    
        if 
            userName = "" ||
            userName.Contains " " ||
            userName.Contains "\"" ||
            userName.Contains "\\" then
            return! fsPage (errorPage "非法的用户名！" "/register") context
        else if password <> password2 then
            return! fsPage (errorPage "两次输入的密码不一致！" "/register") context
        else
            register ({
                name = userName
                password = SHA256.sha256 password
            })
            return! Suave.Redirection.redirect "/" context
    }
