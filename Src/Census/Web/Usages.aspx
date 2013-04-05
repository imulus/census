<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usages.aspx.cs" Inherits="Census.Web.Usages" MasterPageFile="../../masterpages/umbracoPage.master" %>
<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
  <link type="text/css" rel="stylesheet" href="style.css"/>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:TabView ID="TabView1" runat="server" Width="552px" Height="392px"></cc1:TabView>
    <asp:PlaceHolder runat="server" ID="grid"></asp:PlaceHolder>
</asp:Content>