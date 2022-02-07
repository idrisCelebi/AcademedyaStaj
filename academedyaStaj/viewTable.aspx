<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="viewTable.aspx.cs" Inherits="academedyaStaj.viewTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-12 col-md-8 col-lg-6 col-xl-5"style="display:contents">
                <div class="card bg-dark text-white" style="border-radius: 1rem; display: inline-grid">
                    <div class="card-body p-5 text-center">

                        <div class="mb-md-5 mt-md-4 pb-5">
                            <h2 class="fst-normal mb-2 text-uppercase">Veri Görüntüle</h2>
                            <br />
                            <br />
                            <asp:MultiView ID="allviews" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <p class="text-white mb-5">Tablonuzu seçiniz</p>
                                    <asp:DropDownList ID="DropDownList1" CssClass="dropdown-header dropdown-item text-success" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="forwardbutton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="İleri" OnClick="forwardbutton_Click" />
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <asp:Label ID="infotablename" CssClass="text-info mb-5" runat="server" Text=""></asp:Label>
                                    <br />
                                    <br />
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </asp:View>
                                <asp:View ID="View3" runat="server">                                                                  
                                    <div class="form-outline form-white mb-">

                                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                    </div>
                             <br />  
                                    <asp:Button ID="updateButton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="Güncelle" OnClick="updateButton_Click" />
                                </asp:View>
                            </asp:MultiView>
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


