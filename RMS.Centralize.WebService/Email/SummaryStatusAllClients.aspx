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
        <div>
            เรียน<br/><br/>
            ตารางสรุปสถานะของระบบ Monitoring ณ วัน<%=DateTime.Now.ToString("ddddที่ dd MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH")) %>
            <br/><br/>
        </div>
      <asp:Repeater ID="Repeater1" runat="server">
          <HeaderTemplate>
              <table width="900">
              <tr>
                 <th width="40">#</th>
                 <th width="120">Client Code</th>
                 <th width="120">IP Address</th>
                 <th width="220">Location</th>
                 <th width="60">OK</th>
                 <th width="60">Issue</th>
                 <th width="100">Today Biz Message</th>
                 <th width="60">Alive</th>
              </tr>
          </HeaderTemplate>

          <ItemTemplate>
          <tr>
              <td bgcolor="#EEE" style="text-align: center">
                <asp:Label runat="server" ID="Label1" 
                    text='<%# Container.ItemIndex + 1 %>' />
              </td>
              <td bgcolor="#EEE">
                <asp:Label runat="server" ID="Label10" text='<%# Eval("ClientCode") %>' />
              </td>
              <td bgcolor="#EEE">
                <asp:Label runat="server" ID="Label5" 
                    text='<%# Eval("IPAddress") %>' />
              </td>
              <td bgcolor="#EEE">
                <asp:Label runat="server" ID="Label6" 
                    text='<%# Eval("LocationCode") + " - " + Eval("LocationName") %>' />
              </td>
              <td bgcolor="#EEE" style="text-align: center">
                <asp:Label runat="server" ID="Label7" 
                    text='<%# Eval("CounterOK") %>' />
              </td>
              <td bgcolor="#EEE" style="text-align: center">
                  <asp:Label runat="server" ID="Label2" 
                      text='<%# Eval("CounterNotOK") %>' />
              </td>
              <td bgcolor="#EEE" style="text-align: center">
                  <asp:Label runat="server" ID="Label8" 
                      text='<%# Eval("CounterBizMessage") %>' />
              </td>
              <td bgcolor="#EEE" style="text-align: center">
                  <asp:Label runat="server" ID="Label9" 
                      text='<%# (ReferenceEquals(Eval("AgentNotAlive"), "0"))? "Yes" : "No" %>' />
              </td>
          </tr>
          </ItemTemplate>

          <AlternatingItemTemplate>
          <tr>
              <td style="text-align: center">
                <asp:Label runat="server" ID="Label1" 
                    text='<%# Container.ItemIndex + 1 %>' />
              </td>
              <td>
                <asp:Label runat="server" ID="Label10" 
                    text='<%# Eval("ClientCode") %>' />
              </td>
              <td>
                <asp:Label runat="server" ID="Label5" 
                    text='<%# Eval("IPAddress") %>' />
              </td>
              <td>
                <asp:Label runat="server" ID="Label6" 
                    text='<%# Eval("LocationCode") + " - " + Eval("LocationName") %>' />
              </td>
              <td style="text-align: center">
                <asp:Label runat="server" ID="Label7" 
                    text='<%# Eval("CounterOK") %>' />
              </td>
              <td style="text-align: center">
                  <asp:Label runat="server" ID="Label2" 
                      text='<%# Eval("CounterNotOK") %>' />
              </td>
              <td style="text-align: center">
                  <asp:Label runat="server" ID="Label8" 
                      text='<%# Eval("CounterBizMessage") %>' />
              </td>
              <td style="text-align: center">
                  <asp:Label runat="server" ID="Label9" 
                      text='<%# (ReferenceEquals(Eval("AgentNotAlive"), "0"))? "Yes" : "No" %>' />
              </td>
          </tr>
          </AlternatingItemTemplate>

          <FooterTemplate>
              </table>
          </FooterTemplate>
      </asp:Repeater>
    <!--END-->

    </div>
    </form>
</body>
</html>
