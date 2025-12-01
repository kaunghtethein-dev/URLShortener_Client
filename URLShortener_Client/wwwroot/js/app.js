window.AppJS = {
    hideElement: function (element) {
        if (element) {
            element.classList.remove('show');
            // Wait for animation to complete before hiding completely
            setTimeout(() => {
                element.style.display = 'none';
            }, 300);
        }
    },
    showElement: function (element) {
        if (element) {
            element.style.display = 'block';
            // Trigger reflow to ensure transition works
            void element.offsetWidth;
            element.classList.add('show');
        }
    }
};