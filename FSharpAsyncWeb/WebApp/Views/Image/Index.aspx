<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>
    <form action="/image/load" method="get" target="_blank">
        C# Load: <input type="text" name="url" /><input type="submit" />
    </form>

    <form action="/image/loadfs" method="get" target="_blank">
        F# Load: <input type="text" name="url" /><input type="submit" />
    </form>
</body>
</html>
