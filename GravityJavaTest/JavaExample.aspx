<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaExample.aspx.cs" Inherits="GravityJavaTest.JavaExample" %>

<!DOCTYPE html>
<html>

<head>
    <title>Test JAVA</title>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
<script>

    $(document).ready(function () {
        alert("The paragraph is now hidden");

        $("button").click(function () {
        $("p").hide();
      });
    });
</script>

</head>
<body>

<h2>This is a heading</h2>

<p>This is a paragraph.</p>
<p>This is another paragraph.</p>

<button>Click me to hide paragraphs</button>

</body>
</html>
