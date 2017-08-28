<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        SelectMethod="SelectAll" InsertMethod="Insert"
        DeleteMethod="Delete" UpdateMethod="Update">
        <InsertParameters>
            <asp:Parameter Type="Object" Name="item" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Type="Object" Name="item" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Type="Object" Name="item" />
        </DeleteParameters>
    </asp:ObjectDataSource>

    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="Id">
        <Columns>
            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
            <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <asp:FormView ID="FormView1" runat="server" DefaultMode="Insert" DataSourceID="ObjectDataSource1" DataKeyNames="Id">
        <EditItemTemplate>
            Id:
            <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            Telephone:
            <asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Id:
            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            Telephone:
            <asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            Id:
            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
            <br />
            Name:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Email:
            <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            Telephone:
            <asp:Label ID="TelephoneLabel" runat="server" Text='<%# Bind("Telephone") %>' />
            <br />
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
