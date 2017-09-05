<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebForm1" %>
<%@ Register TagPrefix="cv" Namespace="DataManager" Assembly="Example1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <cv:CustomDataSource ID="ObjectDataSource1" runat="server"
         DataObjectTypeName="Customer" SelectMethod="SelectAll">
    </cv:CustomDataSource>

    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="Id" 
        AllowSorting="true" AllowPaging="true" PageSize="3">
        <Columns>
            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" />
            <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <asp:ValidationSummary ID="valSummary" runat="server" />

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
            <cv:AnnotationValidator ID="idval" runat="server" ControlToValidate="IdTextBox" 
                PropertyName="Id" SourceType="Customer" Text="*" />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <cv:AnnotationValidator ID="custval" runat="server" ControlToValidate="NameTextBox" 
                PropertyName="Name" SourceType="Customer" Text="*" />
            <br />       
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
            <cv:AnnotationValidator ID="emailVal" runat="server" ControlToValidate="EmailTextBox"
                 PropertyName="Email" SourceType="Customer" Text="*" />
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
