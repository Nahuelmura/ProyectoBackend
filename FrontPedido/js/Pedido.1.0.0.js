let detallesPedido = []; // donde guardamos los detalles del pedido

function ObtenerPedido() {
  fetch("http://localhost:5049/api/Pedido")
    .then((respuesta) => respuesta.json())
    .then((data) => {
      console.log(data);
      mostrarPedido(data);
    })
    .catch((error) => console.error(error));
}

function mostrarPedido(data) {
  const tbody = document.getElementById("TablaPedido");
  tbody.innerHTML = "";

  data.forEach((element) => {
    console.log("Elemento:", element);
    console.log("ID:", element.pedidoID);
    let tr = tbody.insertRow();
    tr.insertCell(0).innerHTML = element.nombreDelCliente;
    tr.insertCell(1).innerHTML = element.fechaFormateada;
    tr.insertCell(2).innerHTML = element.estadoPedido;
    tr.insertCell(3).innerHTML = element.total;
  });
}

// funcion donde obtenemos los productos
function obtenerProductos() {
  fetch("http://localhost:5049/api/Producto")
    .then((respuesta) => respuesta.json())

    .then((data) => {
      let productos = data.productos || data.Productos; // Agregamos esta variable porque lo traemos envuelto desde el get
      let productoSeleccionado = document.getElementById("producto");

      productoSeleccionado.innerHTML =
        '<option value="">[SELECCIONE...]</option>';

      //el foreach ahora recorre productos
      productos.forEach((producto) => {
        productoSeleccionado.innerHTML += `
                    <option 
                        value="${producto.productoID}"data-precio="${producto.precioVentaProducto}">
                        
                        ${producto.nombreProducto}

                    </option>
                `;
      });
    })

    .catch((error) => {
      console.error("Error al obtener productos:", error);
    });
}

// funcion donde obtenemos los Clientes
function obtenerClientes() {
  fetch(`http://localhost:5049/api/Cliente`)
    .then((respuesta) => respuesta.json())

    .then((data) => {
      let productoSeleccionado = document.getElementById("cliente");

      productoSeleccionado.innerHTML =
        '<option value="">[SELECCIONE...]</option>';

      data.forEach((cliente) => {
        productoSeleccionado.innerHTML += `
                    <option 
                        value="${cliente.clienteID}">
                        
                        ${cliente.nombre}

                    </option>
                `;
      });
    })

    .catch((error) => {
      console.error("Error al obtener productos:", error);
    });
}

function cargarPrecio() {
  let producto = document.getElementById("producto");

  let precioInput = document.getElementById("precioUnitario");

  let opcionSeleccionada = producto.options[producto.selectedIndex];

  if (producto.value == "") {
    precioInput.value = "";
    return;
  }

  let precio = opcionSeleccionada.dataset.precio;

  precioInput.value = precio;
}

function AgregarDetalle() {
  let producto = document.getElementById("producto");
  let cantidad = Number(document.getElementById("cantidad").value);
  let precio = Number(document.getElementById("precioUnitario").value);

  let detalle = {
    productoID: Number(producto.value),
    nombreProducto: producto.options[producto.selectedIndex].text, // lo muestra en pantalla
    cantidad: cantidad,
    precioUnitario: precio,
  };

  detallesPedido.push(detalle);
  MostrarDetalles();
}

function MostrarDetalles() {
  let tbody = document.getElementById("tablaDetalles");
  if (!tbody) return;

  tbody.innerHTML = "";
  let totalAcumulado = 0;

  detallesPedido.forEach((detalle, index) => {
    let subtotal = detalle.cantidad * detalle.precioUnitario;
    totalAcumulado += subtotal;

    tbody.innerHTML += `
      <tr>
        <td>${detalle.nombreProducto}</td>
        <td>${detalle.cantidad}</td>
        <td>$${detalle.precioUnitario.toFixed(2)}</td>
        <td>$${subtotal.toFixed(2)}</td>
        <td><button type="button" onclick="EliminarDetalle(${index})">Eliminar</button></td>
      </tr>
    `;
  });
  let inputTotal = document.getElementById("total");
  if (inputTotal) {
    inputTotal.value = totalAcumulado.toFixed(2);
  }
}
function EliminarDetalle(index) {
  detallesPedido.splice(index, 1);
  MostrarDetalles();
}

function CrearPedido() {
  let pedido = {
    clienteID: document.getElementById("cliente").value,
    fecha: document.getElementById("fecha").value,
    estado: Number(document.getElementById("estado").value),
    detallePedidos: detallesPedido,
  };

  console.log("ENVIANDO PEDIDO:", pedido);

  fetch("http://localhost:5049/api/Pedido", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(pedido),
  })
    .then((respuesta) => respuesta.json())
    .then((data) => {
      console.log("Pedido creado:", data);
      alert("Pedido creado correctamente");

      // iimpiamos el formulario
      detallesPedido = [];
      document.getElementById("fecha").value = "";
      document.getElementById("cliente").value = "";
      document.getElementById("cantidad").value = ""; 
      document.getElementById("precioUnitario").value = "";

      MostrarDetalles();
      obtenerProductos();
      ObtenerPedido();
    })
    .catch((error) => {
      console.error("Error al crear pedido:", error);
      alert("Error al conectar con el servidor.");
    });
}

ObtenerPedido();
obtenerProductos();
obtenerClientes();
