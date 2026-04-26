// ========================================
// Global Utilities
// ========================================

const App = {
      // Toast notifications
      toast: function (message, type = 'success') {
            const toastContainer = document.getElementById('toast-container') || this.createToastContainer();

            const toast = document.createElement('div');
            toast.className = `toast align-items-center text-white bg-${type === 'success' ? 'success' : type === 'error' ? 'danger' : 'warning'}`;
            toast.setAttribute('role', 'alert');

            toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    <i class="bi bi-${type === 'success' ? 'check-circle' : type === 'error' ? 'x-circle' : 'exclamation-triangle'} me-2"></i>
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;

            toastContainer.appendChild(toast);
            const bsToast = new bootstrap.Toast(toast, { delay: 3000 });
            bsToast.show();

            toast.addEventListener('hidden.bs.toast', () => toast.remove());
      },

      createToastContainer: function () {
            const container = document.createElement('div');
            container.id = 'toast-container';
            container.className = 'toast-container position-fixed top-0 start-0 p-3';
            container.style.zIndex = '9999';
            document.body.appendChild(container);
            return container;
      },

      // Confirm delete
      confirmDelete: function (formId, message = 'هل أنت متأكد من الحذف؟') {
            if (confirm(message)) {
                  document.getElementById(formId)?.submit();
            }
      },

      // Format currency
      formatCurrency: function (amount) {
            return new Intl.NumberFormat('ar-EG', {
                  style: 'currency',
                  currency: 'EGP'
            }).format(amount);
      },

      // Format date
      formatDate: function (dateString) {
            return new Date(dateString).toLocaleDateString('ar-EG', {
                  year: 'numeric',
                  month: 'long',
                  day: 'numeric'
            });
      },

      // Loading state for buttons
      setLoading: function (button, loading = true) {
            if (loading) {
                  button.dataset.originalText = button.innerHTML;
                  button.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>جاري التحميل...';
                  button.disabled = true;
            } else {
                  button.innerHTML = button.dataset.originalText;
                  button.disabled = false;
            }
      }
};

// ========================================
// Form Validation Helpers
// ========================================

document.addEventListener('DOMContentLoaded', function () {
      // Bootstrap form validation
      const forms = document.querySelectorAll('.needs-validation');

      forms.forEach(form => {
            form.addEventListener('submit', function (event) {
                  if (!form.checkValidity()) {
                        event.preventDefault();
                        event.stopPropagation();
                  }
                  form.classList.add('was-validated');
            });
      });

      // Auto-hide alerts after 5 seconds
      const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
      alerts.forEach(alert => {
            setTimeout(() => {
                  alert.classList.remove('show');
                  setTimeout(() => alert.remove(), 150);
            }, 5000);
      });
});

// ========================================
// AJAX Helpers (for API calls later)
// ========================================

const API = {
      get: async function (url) {
            try {
                  const response = await fetch(url);
                  if (!response.ok) throw new Error('Network error');
                  return await response.json();
            } catch (error) {
                  App.toast('حدث خطأ في الاتصال', 'error');
                  throw error;
            }
      },

      post: async function (url, data) {
            try {
                  const response = await fetch(url, {
                        method: 'POST',
                        headers: {
                              'Content-Type': 'application/json',
                              'Accept': 'application/json'
                        },
                        body: JSON.stringify(data)
                  });
                  if (!response.ok) throw new Error('Network error');
                  return await response.json();
            } catch (error) {
                  App.toast('حدث خطأ في الاتصال', 'error');
                  throw error;
            }
      }
};