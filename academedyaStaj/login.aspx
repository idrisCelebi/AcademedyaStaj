<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="academedyaStaj.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="tr">
<head runat="server">
<meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Acadamedya-Staj</title>
        <!-- Favicon-->
        <link rel="icon" type="image/x-icon" href="assets/favicon.ico" />
        <!-- Core theme CSS (includes Bootstrap)-->
        <link href="~/css/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <div class="container">
                <asp:Label ID="showwho" CssClass="navbar-brand" Text="Offline" runat="server" ></asp:Label>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"><span class="navbar-toggler-icon"></span></button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                        <!--<li class="nav-item"><a class="nav-link active" aria-current="page" href="#">Home</a></li>
                        <li class="nav-item"><a class="nav-link" href="#">Link</a></li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Dropdown</a>
                            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                                <li><a class="dropdown-item" href="#">Action</a></li>
                                <li><a class="dropdown-item" href="#">Another action</a></li>
                                <li><hr class="dropdown-divider" /></li>
                                <li><a class="dropdown-item" href="#">Something else here</a></li>
                            </ul>
                        </li>
                        -->
                    </ul>
                </div>
            </div>
        </nav>
        <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-12 col-md-8 col-lg-6 col-xl-5">
        <div class="card bg-dark text-white" style="border-radius: 1rem;">
          <div class="card-body p-5 text-center">

            <div class="mb-md-5 mt-md-4 pb-5">

              <h2 class="fw-bold mb-2 text-uppercase">giriş yap</h2>
              <p class="text-white-50 mb-5">Lütfen kullanıcı adı ve şifrenizi giriniz.</p>

              <div class="form-outline form-white mb-4">
                  <asp:TextBox ID="username" CssClass="form-control form-control-lg" runat="server" AutoPostBack="True"></asp:TextBox>
                
                <label class="form-label" for="typeEmailX">Kullanıcı Adı</label>
              </div>

              <div class="form-outline form-white mb-4">
                  <asp:TextBox ID="password" CssClass="form-control form-control-lg" runat="server" TextMode="Password"></asp:TextBox>
                
                <label class="form-label" for="typePasswordX">Şifre</label>
              </div>
                  <div class="form-outline form-white mb-4">
                      <asp:CheckBox ID="rememberme" CssClass="form-check" Text="Beni Hatırla" runat="server" />
                
                
              </div>
             
                 <br />

               <asp:Button ID="loginbutton" CssClass="btn btn-outline-light btn-lg px-5" runat="server" Text="Giriş" OnClick="loginbutton_Click" />
                <br />
                <asp:Label ID="infologin" CssClass="form-label" runat="server" ></asp:Label>
             <!-- <div class="d-flex justify-content-center text-center mt-4 pt-1">
                <a href="#!" class="text-white"><i class="fab fa-facebook-f fa-lg"></i></a>
                <a href="#!" class="text-white"><i class="fab fa-twitter fa-lg mx-4 px-2"></i></a>
                <a href="#!" class="text-white"><i class="fab fa-google fa-lg"></i></a>
              </div>
                 -->

            </div>

            <div>
              <p class="mb-0">Hesabın yok mu? <a href="registerPage.aspx" class="text-white-50 fw-bold">Kayıt Ol</a></p>
            </div>

          </div>
        </div>
      </div>
    </div>
  </div>
    </form>
</body>
</html>
