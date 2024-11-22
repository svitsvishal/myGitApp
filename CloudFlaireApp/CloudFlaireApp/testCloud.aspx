<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testCloud.aspx.cs" Inherits="CloudFlaireApp.testCloud" Async ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://challenges.cloudflare.com/turnstile/v0/api.js?onload=onloadTurnstileCallback"  async defer>


    </script>
    <script type="text/javascript">
  window.onloadTurnstileCallback = function () {
    turnstile.render('#example-container', {
        sitekey: '0x4AAAAAAAfV6EAhE9eHmvhK',//Site key 
        /*size:'invisible',*/
        callback: function(token) {
            console.log(`Challenge Success ${token}`);           
            document.getElementById('hfTurnstileResponse').value = token;
          //  alert(token)
        },
    });
};
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <div id="example-container">
              <div class="cf-turnstile" data-sitekey="0x4AAAAAAAfV6EAhE9eHmvhK"></div><br />
         </div>
        <asp:HiddenField ID="hfTurnstileResponse" runat="server" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </form>
</body>
</html>
