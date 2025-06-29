// site.js
// Show success message after form submission
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

window.onload = function () {
  const urlParams = new URLSearchParams(window.location.search);
  const message = urlParams.get("message");

  if (message) {
    document.getElementById("toastMessage").textContent = message;
    const toast = new bootstrap.Toast(
      document.getElementById("notificationToast")
    );
    toast.show();

    // Clear the message from URL
    const url = new URL(window.location);
    url.searchParams.delete("message");
    window.history.replaceState({}, document.title, url.toString());
  }
};

// Show equipment details
function showDetail(
  id,
  name,
  type,
  location,
  quantity,
  value,
  condition,
  installedDate
) {
  document.getElementById("detailId").textContent = id;
  document.getElementById("detailName").textContent = name;
  document.getElementById("detailType").textContent = type;
  document.getElementById("detailLocation").textContent = location;
  document.getElementById("detailQuantity").textContent = quantity;
  document.getElementById("detailValue").textContent =
    "$" + parseFloat(value).toLocaleString();
  document.getElementById("detailCondition").textContent = condition + "%";
  document.getElementById("detailInstalledDate").textContent = installedDate;
}

// Load edit form with equipment data
function loadEditForm(
  id,
  name,
  type,
  location,
  quantity,
  value,
  condition,
  status,
  installedDate
) {
  document.getElementById("editId").value = id;
  document.getElementById("editName").value = name;
  document.getElementById("editType").value = type;
  document.getElementById("editLocation").value = location;
  document.getElementById("editQuantity").value = quantity;
  document.getElementById("editValue").value = value;
  document.getElementById("editCondition").value = condition;
  document.getElementById("editStatus").value = status;
  document.getElementById("editInstalledDate").value = installedDate;
}

// Search functionality
document.getElementById("searchInput").addEventListener("input", function () {
  const searchTerm = this.value.toLowerCase();
  const cards = document.querySelectorAll(".equipment-card");

  cards.forEach((card) => {
    const name = card.querySelector(".card-title").textContent.toLowerCase();
    const id = card
      .querySelector(".card-text:nth-child(2)")
      .textContent.split(": ")[1]
      .toLowerCase();

    if (name.includes(searchTerm) || id.includes(searchTerm)) {
      card.closest(".col-12").style.display = "block";
    } else {
      card.closest(".col-12").style.display = "none";
    }
  });
});

// Filter by category
document
  .getElementById("categoryFilter")
  .addEventListener("change", function () {
    const category = this.value;
    filterEquipment(category, "category");
  });

// Filter by status
document.getElementById("statusFilter").addEventListener("change", function () {
  const status = this.value;
  filterEquipment(status, "status");
});

function filterEquipment(value, type) {
  const cards = document.querySelectorAll(".equipment-card");

  cards.forEach((card) => {
    const cardValue =
      type === "category"
        ? card
            .querySelector(".card-text:nth-child(3)")
            .textContent.split(": ")[1]
            .toLowerCase()
        : card.querySelector(".status-badge").textContent.toLowerCase();

    if (!value || cardValue === value.toLowerCase()) {
      card.closest(".col-12").style.display = "block";
    } else {
      card.closest(".col-12").style.display = "none";
    }
  });
}
