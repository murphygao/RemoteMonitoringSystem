<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryStatusAllClients.aspx.cs" Inherits="RMS.Centralize.WebService.Email.SummaryStatusAllClients" %>

<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="content-type" content="text/html; charset=UTF-8">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <!--BEGIN-->
            <meta http-equiv="content-type" content="text/html; charset=UTF-8">
            <div style="font-size: 10pt">
                <div>
                    เรียน<br />
                    <br />
                    ตารางสรุปสถานะของระบบ Monitoring ณ วัน<%=DateTime.Now.ToString("ddddที่ dd MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH")) %>
                    <br />
                    <br />
                </div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table width="900">
                            <tr>
                                <th width="40" style="text-align: center; font-weight: bold">#</th>
                                <th width="120" style="font-weight: bold">Client Code</th>
                                <th width="120" style="font-weight: bold">IP Address</th>
                                <th width="220" style="font-weight: bold">Location</th>
                                <th width="80" style="text-align: center; font-weight: bold">OK Message</th>
                                <th width="80" style="text-align: center; font-weight: bold">Issue Message</th>
                                <th width="100" style="text-align: center; font-weight: bold">Today Biz Message</th>
                                <th width="200" style="font-weight: bold">Status</th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center; background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label1"
                                    Text='<%# Container.ItemIndex + 1 %>' />
                            </td>
                            <td style="background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label10" Text='<%# Eval("ClientCode") %>' />
                            </td>
                            <td style="background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label5"
                                    Text='<%# Eval("IPAddress") %>' />
                            </td>
                            <td style="background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label6"
                                    Text='<%# Eval("LocationCode") + " - " + Eval("LocationName") %>' />
                            </td>
                            <td style="text-align: center; background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label7"
                                    Text='<%# Eval("CounterOK") %>' />
                            </td>
                            <td style="text-align: center; background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label2"
                                    Text='<%# Eval("CounterNotOK") %>' />
                            </td>
                            <td style="text-align: center; background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label8"
                                    Text='<%# Eval("CounterBizMessage") %>' />
                            </td>
                            <td style="background-color: #eeeeee">
                                <asp:Label runat="server" ID="Label9" Text='<%# Eval("sRMSStatus") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label runat="server" ID="Label1"
                                    Text='<%# Container.ItemIndex + 1 %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label10"
                                    Text='<%# Eval("ClientCode") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label5"
                                    Text='<%# Eval("IPAddress") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label6"
                                    Text='<%# Eval("LocationCode") + " - " + Eval("LocationName") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label runat="server" ID="Label7"
                                    Text='<%# Eval("CounterOK") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label runat="server" ID="Label2"
                                    Text='<%# Eval("CounterNotOK") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label runat="server" ID="Label8"
                                    Text='<%# Eval("CounterBizMessage") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label9"
                                    Text='<%# Eval("sRMSStatus") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <!--END-->

        </div>
    </form>
</body>
</html>
