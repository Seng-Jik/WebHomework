module Pages.MasterPage
open Pages.Components
open Bootstrap
open FsHtml

let master (user:Model.Users.User option) page =
    bsPage 
        [
            title %"新闻管理系统"
        ]
        [
            elem "nav" [
                "class" %= "navbar navbar-expand-lg navbar-dark bg-dark"
                a [
                    "class" %= "navbar-brand"
                    "href" %= "#"
                    Text "新闻管理系统"
                ]

                if user.IsSome then 
                    div [

                        ul [
                            "class" %= "navbar-nav"
                            Text " "
                        ]

                        div [
                            elem "form" [
                                "class" %= "form-inline"
                                "action" %= "/exit"
                                "method" %= "post"

                                span [
                                    "class" %= "navbar-text"
                                    "style" %= "margin-right:20px"
                                    Text (sprintf "你好！%s" user.Value.name)
                                ]

                                a [
                                    "type" %= "submit"
                                    "class" %= "btn btn-dark form-control"
                                    "href" %= "/newseditor"
                                    Text "新建文章"
                                ]

                                elem "button" [
                                    "type" %= "submit"
                                    "class" %= "btn btn-dark form-control"
                                    Text "退出登录"
                                ]
                            ]
                        ]
                    ]
            ]

            div page

            elem "footer" [
                "style" %= "color:#787878;padding-top:20px;padding-bottom:20px;text-align:center;"
                bsCotainer [
                    p [
                        "style" %= "color:#787878;"
                        Text "新闻管理系统"
                    ]
                ]
            ]
        ]
