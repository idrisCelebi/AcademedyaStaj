<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addData.aspx.cs" Inherits="academedyaStaj.addData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-12 col-md-8 col-lg-6 col-xl-5">
        <div class="card bg-dark text-white" style="border-radius: 1rem;">
          <div class="card-body p-5 text-center">

            <div class="mb-md-5 mt-md-4 pb-5">
              <h2 class="fst-normal mb-2 text-uppercase">Veri Ekleme Ekranı</h2>
                <br />
                 <br />    
                
                 
               


                    <asp:MultiView ID="allviews" runat="server">

                        <asp:View ID="View1" runat="server" OnLoad="View1_Load">
                            <p class="text-white mb-5">Tablonuzu seçiniz</p>
                           
                                 <asp:DropDownList  ID="DropDownList1" CssClass="dropdown-header dropdown-item text-success" runat="server"></asp:DropDownList>

                      
                      <br />
                 <br />
                            
                 <br />    
                        <asp:Button ID="forwardbutton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="İleri" OnClick="forwardbutton_Click"  />

                        </asp:View>
                        <asp:View ID="View2" runat="server" OnLoad="View2_Load">
                            <asp:Label ID="infotablename" CssClass="text-info mb-5" runat="server" Text=""></asp:Label>
                            <br /><br />
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                <asp:Table ID="Table1" runat="server"></asp:Table>
                            </asp:PlaceHolder>
                                <br />
                 <br />
                            
                 <br />    
                        <asp:Button ID="addButton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="Veri Ekle" OnClick="addButton_Click" />
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
