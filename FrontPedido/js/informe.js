function cargarInforme() {
  fetch("http://localhost:5049/api/Informe/InformeClientes")
    .then((respuesta) => respuesta.json())
    .then((clientes) => {
      let contenido = document.getElementById("contenidoInforme");
      contenido.innerHTML = "";

      clientes.forEach((cliente) => {
        // 1. Fila nivel 1: Cliente (resaltada en gris con texto en negrita)
        let filaCliente = `
          <tr class="table-secondary fw-bold">
            <td>${cliente.clienteID}</td>
            <td>${cliente.nombreCompleto}</td>
            <td>${cliente.telefono ?? "S/D"}</td>
            <td>${cliente.email ?? "S/D"}</td>
            <td>$${cliente.total ?? 0}</td>
          </tr>
        `;

        // 2. Fila nivel 2: Pedidos de ese cliente
        let filasPedidos = "";

        if (cliente.pedidos && cliente.pedidos.length > 0) {
          cliente.pedidos.forEach((pedido) => {
            filasPedidos += `
              <tr>
                <td>#${pedido.pedidoID}</td>
                <td>${pedido.fechaFormateada}</td>
                <td><span class="badge bg-info text-dark">${pedido.estadoPedido}</span></td>
                <td>$${pedido.total ?? 0}</td>
              </tr>
            `;
          });
        } else {
          filasPedidos = `
            <tr>
              <td colspan="4" class="text-center text-muted fst-italic py-2">
                Este cliente no registra pedidos.
              </td>
            </tr>
          `;
        }

        // Contenedor de la subtabla con sangría a la izquierda (ps-4)
        let subTablaPedidos = `
          <tr>
            <td colspan="5" class="p-3 ps-4 bg-light">
              <table class="table table-sm table-bordered mb-0 bg-white">
                <thead class="table-light">
                  <tr>
                    <th>N° Pedido</th>
                    <th>Fecha</th>
                    <th>Estado</th>
                    <th>Total</th>
                  </tr>
                </thead>
                <tbody>
                  ${filasPedidos}
                </tbody>
              </table>
            </td>
          </tr>
        `;

        contenido.innerHTML += filaCliente + subTablaPedidos;
      });
    })
    .catch((error) => console.error("Error al cargar el informe:", error));
}

cargarInforme();
