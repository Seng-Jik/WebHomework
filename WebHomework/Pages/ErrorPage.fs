module Pages.ErrorPage

open Pages.Components.Bootstrap
open FsHtml
open Model.Users

let errorPage message backforwardUrl =
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
                        h1 %"错误"
                        p %message

                        a [
                            "class" %= "btn btn-dark col-5"
                            "href" %= backforwardUrl
                            Text "返回"
                        ]
                    ]
                ]
            ]
        ]
    ]

