module Pages.NewsEditor

open FsHtml
open Pages.Components.Common
open Model.Users
open Suave.Http
open Suave.State.CookieStateStore

let newsEditor user =
    MasterPage.master (Some user) [
        Components.Bootstrap.bsCotainer [
            elem "form" [
                "method" %= "post"

                elem "input" [
                    "type" %= "text"
                    "class" %= "form-control"
                    "style" %= "margin-top:20px"
                    "placeholder" %= "文章标题"
                    "name" %= "title"
                ]

                Text """
                    <textarea
                        class="form-control"
                        placeholder="文章标题"
                        style="margin-top:20px"
                        rows="20"
                        name="content"
                        name="title"></textarea>
                  """
                

                div [
                    "class" %= "row"
                    "style" %= "margin-top:20px"

                    div [
                        "class" %= "col-6"
                        elem "center" [
                            elem "button" [
                                "class" %= "btn btn-dark"
                                "type" %= "submit"
                                Text "发表"
                            ]
                        ]
                    ]

                    div [
                        "class" %= "col-6"
                        elem "center" [
                            a [
                                "class" %= "btn btn-dark"
                                "href" %= "/newslist"
                                Text "返回"
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]

open Model.News

let postNews (request:HttpRequest) context =
    async {
        {
            title = request.["title"].Value
            content = request.["content"].Value
        }
        |> addNews
        return! Suave.Redirection.redirect "/newslist" context
    }
