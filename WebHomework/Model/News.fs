module Model.News

type NewsID = int
type NewsTitle = string

type News = {
    title : NewsTitle
    content : string
}

let private newsTable = Model.Connection.Database.TbNews

let getNewsList () : (NewsID * NewsTitle) seq =
    query {
        for news in newsTable do
        select (news.Id,news.Title)
    }
    |> Seq.rev


let getNewsByID id =
    query {
        for news in newsTable do
        where (news.Id = id)
        select (news.Title,news.Contents)
    }
    |> Seq.map (fun x -> {
        title = fst x
        content = snd x
    })
    |> Seq.tryHead

let addNews news =
    newsTable.Create([
        "title",news.title :> obj
        "contents",news.content :> obj
    ]) |> ignore
    Model.Connection.SqlContext.SubmitUpdates()
