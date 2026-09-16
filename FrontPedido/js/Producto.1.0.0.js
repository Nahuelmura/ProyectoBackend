
function obtenerCategorias() {
  fetch("http://localhost:5049/api/Producto/idCategorias")
    .then((respuesta) => respuesta.json())
    .then((data) => {
      let opciones = '<option value="">[SELECCIONE...]</option>';

      data.forEach((categoria) => {
        opciones += `
                    <option value="${categoria.id}">
                        ${categoria.nombre}
                    </option>
                `;
      });

      document.getElementById("CategoriaID").innerHTML = opciones;
      document.getElementById("CategoriaIDeditar").innerHTML = opciones;
    })
    .catch((error) => console.error("Error categorías:", error));
}

obtenerCategorias();
