module Pages.NewsList

open FsHtml
open Pages.Components.Common
open Model.Users
open Suave.Http
open Suave.State.CookieStateStore

let newslist user =
    MasterPage.master (Some user) [
        div [
            "class" %= "list-group"
            "style" %= "margin-top:20px"

            Model.News.getNewsList ()
            |> Seq.map (fun (id,newsTitle) ->
                a [
                    "href" %= sprintf "/newsviewer?id=%d" id
                    "class" %= "list-group-item list-group-item-action"
                    Text newsTitle
                ])
            |> Seq.toList
            |> Components.Bootstrap.bsCotainer
        ]
    ]
