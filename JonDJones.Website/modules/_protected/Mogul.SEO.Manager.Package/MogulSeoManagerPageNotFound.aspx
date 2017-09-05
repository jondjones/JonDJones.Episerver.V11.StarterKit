<%@ Page Language="C#" AutoEventWireup="true" %>
<script runat="server">
    protected NameValueCollection MetaData = new NameValueCollection();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.TrySkipIisCustomErrors = true;
        try {
           Response.Status = "404 Not Found";
           Response.StatusCode = 404;
        }
        catch {}

        foreach (var key in Request.QueryString.AllKeys)
        {
            MetaData.Add(key, Request.QueryString[key]);
        }
    }
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>404 page</title>
    <style type="text/css">
        html, button, input, select, textarea {
            color: #222;
            font-family: sans-serif;
        }
        body {
            font-size: 1em;
            line-height: 1.4;
            margin: 0;
        }
        div.main {
            padding: 25px;
        }
        div#content {
            border: 1px solid #d0d0d0;
            padding: 45px 60px !important;
        }
        .homeCol1 {
            float: left;
            width: 45%;
        }
        .homeCol2 {
            float: right;
            border-left: 1px solid #d0d0d0;
            padding-left:5%;
            width: 45%;
        }
        div#content p {
            font-size: 13px;
        }
        img {
            border: 0 none;
            vertical-align: middle;
        }
        .inner {
            width:100%;
            margin-top:5px;
        }
        .clearfix {
            clear:both;
        }
    </style>
</head>
<body>
    <div class="main">
        <div id="content">
            <strong>404 - Page not found</strong>
            <div class="inner">
                <div class="homeCol1">
                    <p><strong>Page not found. We hope that you can find related information here that could match what you are looking for.</strong></p>
                    <strong>Meta data:</strong>
                    <p>
                    <% foreach (string metaData in MetaData)
                    {
                        %><%= metaData %>=<%= MetaData[metaData] %><br/><%
                    } %>
                    </p>
                </div>
                <div class="homeCol2">
                    <strong>Related products/content for:</strong>
                    <p>
                        <a href="/"><img src="http://commerce.episerver.com/images/t/5-876-PrimaryImage.image.ashx?epslanguage=en-US" alt="" /></a>
                        <a href="/"><img src="http://commerce.episerver.com/images/t/7-877-PrimaryImage.image.ashx?epslanguage=en-US" alt="" /></a>
                        <a href="/"><img src="http://commerce.episerver.com/images/t/7-878-PrimaryImage.image.ashx?epslanguage=en-US" alt="" /></a>
                    </p>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</body>
</html>
