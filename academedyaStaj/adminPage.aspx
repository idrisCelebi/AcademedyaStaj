<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="adminPage.aspx.cs" Inherits="academedyaStaj.adminPage" %>


<%@ Register Assembly="DevExpress.Web.v21.2, Version=21.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-12 col-md-8 col-lg-6 col-xl-5"style="display:contents">
                <div class="card bg-dark text-white" style="border-radius: 1rem; display: inline-grid">
                    <div class="card-body p-5 text-center">

                        <div class="mb-md-5 mt-md-4 pb-5">
                            <h2 class="fst-normal mb-2 text-uppercase">Admin Paneli</h2>
                        <dx:ASPxGridView ID="grid"  Theme="Office2010Silver" KeyFieldName="id" AutoGenerateColumns="False" EnableRowsCache="False" runat="server" OnRowUpdating="grid_RowUpdating" OnStartRowEditing="grid_StartRowEditing" OnEditFormLayoutCreated="grid_EditFormLayoutCreated" OnHtmlEditFormCreated="grid_HtmlEditFormCreated1" OnRowCommand="grid_RowCommand" DataSourceID="SqlDataSource1">    
                            <Columns>
                                <dx:GridViewCommandColumn  VisibleIndex="0" ButtonRenderMode="Button" ButtonType="Button" ShowEditButton="True">
                                </dx:GridViewCommandColumn>
                                 <dx:GridViewDataTextColumn FieldName="id" ReadOnly="True" VisibleIndex="1">
                                      <EditFormSettings Visible="False" />
                                     </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="firstname" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="lastname" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="email" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="username" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="activation" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="admin" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
        </Columns>
     <SettingsEditing EditFormColumnCount="3" Mode="Inline" />
                            <SettingsCommandButton>
                                <EditButton Text="Düzenle"></EditButton>
                                </SettingsCommandButton>
        <EditFormLayoutProperties>
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="768" />
        </EditFormLayoutProperties>
                            <SettingsDataSecurity AllowReadUnexposedColumnsFromClientApi="True" AllowReadUnlistedFieldsFromClientApi="True" PreventLoadClientValuesForInvisibleColumns="True" PreventLoadClientValuesForReadOnlyColumns="True" />
        <SettingsPopup>
            <EditForm Width="730">
                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" />
            </EditForm>

<FilterControl AutoUpdatePosition="False"></FilterControl>
        </SettingsPopup>
        <SettingsPager Mode="ShowAllRecords" />

                        </dx:ASPxGridView>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:akaStajIdrisConnectionString %>" SelectCommand="SELECT [id], [firstname], [lastname], [email], [username], [activation], [admin] FROM [Users]"></asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
