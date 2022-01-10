<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Tables.aspx.cs" Inherits="academedyaStaj.Tables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
            <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                <div class="card bg-dark text-white" style="border-radius: 1rem;">
                    <div class="card-body p-5 text-center">

                        <div class="mb-md-5 mt-md-4 pb-5">

                            <h2 class="fw-bold mb-2 text-uppercase">Tablolarım</h2>

                            <div class="form-outline form-white mb-4">

                                <ul class="list-group" id="tablelist">
                                    <asp:ListBox ID="tables" CssClass="list-group" runat="server"></asp:ListBox>

                                </ul>



                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="form-outline form-white mb-4">

                                <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Tablo Oluştur" Height="56px" ImageUrl="~/assets/system-database-add-icon.png" Width="80px" OnClick="ImageButton1_Click" />
                                <p class="text-white-50 mb-5">Tablo Oluştur</p>
                            </div>
                            <br />



                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
