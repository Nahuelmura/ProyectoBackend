function obtenerCliente() {
  fetch("http://localhost:5049/api/Cliente")
    .then((respuesta) => respuesta.json())
    .then((data) => {
      console.log(data);

      mostrarCliente(data);
    })
    .catch((error) => console.error(error));
}

function mostrarCliente(data) {
  const tbody = document.getElementById("tablaCliente");

  tbody.innerHTML = "";

  data.forEach((element) => {
    console.log(data);
    let tr = tbody.insertRow();
    tr.insertCell(0).innerHTML = element.nombre;
    tr.insertCell(1).innerHTML = element.apellido;
    tr.insertCell(2).innerHTML = element.telefono;
    tr.insertCell(3).innerHTML = element.email;
    tr.insertCell(4).innerHTML = element.direccion;

    // Crear botón
    let editar = document.createElement("button");

    editar.textContent = "Editar";
    editar.classList.add("btn", "btn-primary");

    editar.setAttribute(
      "onclick",
      `BuscarValoresProductos(${element.clienteID})`,
    );

    // Celda del botón
    let tdEditar = tr.insertCell(5);
    tdEditar.appendChild(editar);

    let eliminar = document.createElement("button");
    eliminar.textContent = "Eliminar";
    eliminar.classList.add("btn", "btn-danger");

    eliminar.onclick = function () {
      ValidacionEliminarproducto(element.clienteID);
    };

    let tdEliminar = tr.insertCell(6);
    tdEliminar.appendChild(eliminar);
  });
}

function AgregarCliente() {
  var nuevoCliente = {
    nombre: document.getElementById("nombre").value,
    apellido: document.getElementById("apellido").value,
    telefono: document.getElementById("telefono").value,
    email: document.getElementById("email").value,
    direccion: document.getElementById("direccion").value,
  };

  fetch("http://localhost:5049/api/Categoria", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(nuevaCategoria),
  })
    .then((respuesta) => respuesta.json())
    .then((data) => {
      document.getElementById("nombre").value = "";
      document.getElementById("apellido").value = "";
      document.getElementById("telefono").value = "";
      document.getElementById("email").value = "";
      document.getElementById("direccion").value = "";
      obtenerCliente();
    });
}

obtenerCliente();
