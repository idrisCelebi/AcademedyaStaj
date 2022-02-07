<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="editTable.aspx.cs" Inherits="academedyaStaj.editTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-12 col-md-8 col-lg-6 col-xl-5" style="display:contents">
        <div class="card bg-dark text-white" style="border-radius: 1rem;">
          <div class="card-body p-5 text-center">
            <div class="mb-md-5 mt-md-4 pb-5">
              <h2 class="fst-normal mb-2 text-uppercase">Tablo Düzenleme</h2>
                <br />
                 <br />                   
                    <asp:MultiView ID="allviews" runat="server">
                        <asp:View ID="View1" runat="server" OnLoad="View1_Load">
                            <p class="text-white mb-5">Tablonuzu seçiniz</p>                         
                                 <asp:DropDownList  ID="DropDownList1" CssClass="dropdown-header dropdown-item text-success" runat="server"></asp:DropDownList>
                      <br />    <br />         <br />  
                            <asp:Button ID="edit" CssClass="btn btn-outline-info btn-lg px-5" runat="server" Text="Düzenle" OnClick="edit_Click"   />&nbsp
                        <asp:Button ID="delete" CssClass="btn btn-outline-danger btn-lg px-5" runat="server" Text="  Sil   " OnClientClick="return confirm('Bu tabloyu silmek istediğinden emin misin?');" OnClick="delete_Click"    />
                        </asp:View>
                        <asp:View ID="View2" runat="server" OnLoad="View2_Load" >            
                <asp:Label ID="infotablename" CssClass="text-info mb-5" runat="server" Text=""></asp:Label>
                            <br /><br />
                            <label class="col-form-label-lg">Sütun Adı</label>
                          &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp
                        <label class="col-form-label-lg">Sütun Tipi</label>
                            <div class="form-outline form-white mb-">

                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </div>
                             <br />  
                            <div class="form-outline form-white mb-">
                                        <asp:Button ID="addcolumnone" CssClass="btn btn-outline-light btn-sm" runat="server" Text="Sütun Ekle" CausesValidation="false" OnClick="addcolumnone_Click" />
                              
                                    </div>
                                 <br />
                             <br />
                            <div class="form-outline form-white mb-">
                                      
                                <asp:Button ID="Button3" CssClass="btn btn-outline-success btn-lg px-5" runat="server" Text="Tamamla" OnClick="change_Click"    />
                                    </div>
                             <br />
                            <asp:Label ID="infochangetable" CssClass="form-label" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="View3" runat="server" OnLoad="View3_Load" >            
                <asp:Label ID="Label1" CssClass="text-info mb-5" runat="server" Text=""></asp:Label>
                            <br /><br />
                            <label class="col-form-label-lg">Sütun Adı</label>
                          &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        &nbsp&nbsp&nbsp
                        <label class="col-form-label-lg">Sütun Tipi</label>
                            <div class="form-outline form-white mb-">

                                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                    </div>
                             <br />                          
                                 <br />
                 
                            <div class="form-outline form-white mb-">
                                      
                                <asp:Button ID="finishaddColumn" CssClass="btn btn-outline-success btn-lg px-5" runat="server" Text="Ekle" OnClick="finishaddColumn_Click"   />
                                    </div>
                             <br />
                            <asp:Label ID="Label2" CssClass="form-label" runat="server"></asp:Label>
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
