(function () {
  function cargarLayout() {
    if (document.getElementById("navbarContenido")) return;

    // Normalizar la ruta actual
    const path = window.location.pathname.replace(/\\/g, "/");
    const enViews = path.toLowerCase().includes("/views/");
    const baseRaiz = enViews ? "../" : "./";
    const baseViews = enViews ? "./" : "views/";

    // Determinar la página activa
    const paginaActual = path.split("/").pop().toLowerCase();
    const esInicio = paginaActual === "index.html" || paginaActual === "";
    const esProducto = paginaActual.startsWith("producto");
    const esCategoria = paginaActual.startsWith("categoria");
    const esPedido = paginaActual.startsWith("pedido");

    // Plantilla del Navbar
    const navbarHTML = `
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark mb-4 shadow">
            <div class="container">
                <a class="navbar-brand fw-bold d-flex align-items-center gap-2" href="${baseRaiz}index.html">
                    <span>📦</span> Sistema de Pedidos
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarContenido" aria-controls="navbarContenido" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarContenido">
                    <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <a class="nav-link ${esInicio ? "active fw-semibold" : ""}" href="${baseRaiz}index.html">Inicio</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link ${esProducto ? "active fw-semibold" : ""}" href="${baseViews}Producto.html">Productos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link ${esCategoria ? "active fw-semibold" : ""}" href="${baseViews}Categoria.html">Categorías</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link ${esPedido ? "active fw-semibold" : ""}" href="${baseViews}Pedido.html">Pedidos</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>`;

    // Plantilla del Footer
    const footerHTML = `
        <footer class="bg-white border-top py-3 mt-5 text-center text-muted">
            <div class="container">
                <small>&copy; ${new Date().getFullYear()} Sistema de Pedidos - Panel de Gestión</small>
            </div>
        </footer>`;

    document.body.insertAdjacentHTML("afterbegin", navbarHTML);
    document.body.insertAdjacentHTML("beforeend", footerHTML);
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", cargarLayout);
  } else {
    cargarLayout();
  }
})();
