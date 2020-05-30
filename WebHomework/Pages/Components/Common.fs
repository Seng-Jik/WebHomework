module Pages.Components.Common

open FsHtml
open Suave.Successful

let fsPage = FsHtml.Html.toString >> (+) "<!DOCTYPE html>" >> OK

let meta = 
    Text """<meta charset="UTF-8">"""
    
let importStyle styleName =
     Text (sprintf """<link rel="stylesheet" href="css/%s.css">""" styleName)

let inlineStyle style =
    Text (sprintf """<style type = “text/css”>%s</style>""" style)

let icon iconPath = 
    Text (sprintf """<link rel="icon" href="%s" type="image/x-icon" /><link rel="shortcut icon" href="%s" type="image/x-icon"/>""" iconPath iconPath)