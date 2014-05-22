<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfTesting.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.SelfTesting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table, th, td {
            border: 1px solid black;
            font-family: tahoma;
            font-size: 10pt;
        }
        .title {
            width: 450px;
        }
        .detail {
            width: 250px;
        }
        .result {
            width: 650px
        }
        th, td {
            padding: 5px;
            vertical-align: text-top;
        }
        .txt-red {
            color: red;
        }
        .txt-green {
            color: green;
        }
    </style>
</head>
<body>

    <div>
        <table>
            <tr>
                <th class="title">Check List</th>
                <th class="result">Result</th>
            </tr>
            <tr>
                <td class="title">Check Web Server - Web.config</td>
                <td class="result"><label id="rWebConfig" runat="server"></label></td>
            </tr>
            <tr>
                <td class="title">Check Web Server -> Web Service Server Connection</td>
                <td class="result"><label id="rWebToWsConnection" runat="server"></label></td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server - Web.Config</td>
                <td class="result"><label id="rWebServiceConfig" runat="server"></label></td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Database Server Connection</td>
                <td class="result"><label id="rWSToDbConnection" runat="server"></label></td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Monitoring Agent Connection
                    <br />
                    (*Need one monitoring agent started)</td>
                <td class="result"><label id="rWsToAgentConnection" runat="server"></label></td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Email & SMS Server Connection</td>
                <td class="result"><label id="rWsToEmailConnection" runat="server"></label></td>
            </tr>
        </table>
    </div>

</body>
</html>
