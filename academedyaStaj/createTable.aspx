<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="createTable.aspx.cs" Inherits="academedyaStaj.createTable" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                <div class="card bg-dark text-white" style="border-radius: 1rem;">
                    <div class="card-body p-5 text-center">

                        <div class="mb-md-5 mt-md-4 pb-5">

                            <h2 class="fst-normal mb-2 text-uppercase">Tablo oluşturma ekranı</h2>
                            <br />
                            <br />
                            <br />
                            <asp:MultiView ID="Allviews" runat="server">

                                <asp:View ID="tablenameview" runat="server">
                                    <div class="form-outline form-white mb-">
                                        <asp:TextBox ID="tablename" CssClass="form-control form-control-lg" placeholder="Tablo Adı Giriniz.." runat="server"></asp:TextBox>

                                        <label class="form-label" for="typeEmailX"></label>
                                        <asp:RequiredFieldValidator ID="tablenamevalidator" runat="server" ControlToValidate="tablename" Display="Dynamic" ErrorMessage="Tablo adı boş olamaz" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Bu isimle daha önceden bir tablo oluşturulmuş." ControlToValidate="tablename" Display="Dynamic" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                                        <br />
                                        <asp:Button ID="forwardbutton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="İleri" OnClick="forwardbutton_Click" />
                                    </div>
                                </asp:View>

                                <asp:View ID="columnview" runat="server" OnLoad="columnview_Load" ValidateRequestMode="Enabled">
                                    <label class="col-form-label-lg">Sütun Adı</label>
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp
                        <label class="col-form-label-lg">Sütun Tipi</label>
                                    <div class="form-outline form-white mb-">

                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="form-outline form-white mb-">
                                        <asp:Button ID="addcolumn" CssClass="btn btn-outline-light btn-sm" runat="server" Text="Sütun Ekle" CausesValidation="false" OnClick="addcolumn_Click" />

                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="form-outline form-white mb-">


                                        <asp:Button ID="finishcreateTable" CssClass="btn btn-outline-success btn-lg px-5" runat="server" Text="Tabloyu Oluştur" OnClick="finishcreateTable_Click" />
                                        <br />

                                        <asp:Label ID="infocreatetable" CssClass="form-label" runat="server"></asp:Label>
                                    </div>
                                </asp:View>
                            </asp:MultiView>




                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
