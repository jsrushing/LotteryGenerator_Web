<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LotteryGenerator_Web.Default"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:TextBox runat="server" ID="TextBox1" Width="16" MaxLength="2" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TextBox2" Width="16" MaxLength="2" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TextBox3" Width="16" MaxLength="2" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TextBox4" Width="16" MaxLength="2" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TextBox5" Width="16" MaxLength="2" Visible="false"></asp:TextBox>
        &nbsp;<%--<label>MB:&nbsp;</label>--%><asp:TextBox runat="server" ID="TextBox6" Width="16" Visible="false"></asp:TextBox>
        &nbsp;<label>(n)</label> <asp:TextBox runat="server" ID="txtShuffle" Width="70">50000</asp:TextBox>
        &nbsp;<label>(g)</label> <asp:TextBox runat="server" ID="txtGroupSize" Width="70">5000</asp:TextBox>
        &nbsp;<label>(o)</label> <asp:TextBox runat="server" ID="txtOutputNumber" Width="70">10</asp:TextBox>

        <div>
            <br />
            <asp:Button runat="server" ID ="btnGenerate" Text ="Generate" OnClick="btnGenerate_Click" />
<%--            <asp:Label runat="server" ID="lblNumFound"></asp:Label>--%>
        </div>
    </div>
    <div>
        <br />
        <asp:ListBox runat="server" ID="lstFinal" Height="300" Width="200"></asp:ListBox>
    </div>


</asp:Content>
