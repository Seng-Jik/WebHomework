module Pages.NewsViewer

open FsHtml
open Pages.Components.Common
open Model.Users
open Suave.Http
open Model.News
open Suave.State.CookieStateStore

let newsViewer news user =
    MasterPage.master (Some user) [
        Components.Bootstrap.bsCotainer [
            elem "center" [
                h1 %news.title
            ]

            div [
                "class" %= "card"
                Text news.content
            ]


            elem "center" [
                a [
                    "class" %= "btn btn-dark"
                    "style" %= "margin-top:10px"
                    "href" %= "/newslist"
                    Text "返回"
                ]
            ]
        ]
    ]

let requestViewer (form:HttpRequest) (context:HttpContext) =
    async {
        match context with
        | Model.Users.UserInformation user ->
            match form.["id"] with
            | Some x ->
                try
                    match getNewsByID (int x) with
                    | Some x -> 
                        return! fsPage (newsViewer x user) context
                    | None ->
                        return! Suave.Redirection.redirect "/newslist" context
                with _ ->
                    return! Suave.Redirection.redirect "/newslist" context
            | None ->
                return! Suave.Redirection.redirect "/newslist" context
        | _ -> 
            return! Suave.Redirection.redirect "/exit" context
    }