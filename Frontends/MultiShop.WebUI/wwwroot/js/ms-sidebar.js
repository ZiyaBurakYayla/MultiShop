(function () {
    var storageKey = 'ms-nav-open';

    function apply(open) {
        if (open) {
            document.body.classList.add('ms-nav-open');
        } else {
            document.body.classList.remove('ms-nav-open');
        }
    }

    function init() {
        var sidebar = document.querySelector('.ms-sidebar');
        if (!sidebar) {
            return;
        }

        var stored = null;
        try {
            stored = window.localStorage.getItem(storageKey);
        } catch (e) {
            stored = null;
        }
        if (stored === '1') {
            apply(true);
        }

        var backdrop = document.createElement('div');
        backdrop.className = 'ms-nav-backdrop';
        document.body.appendChild(backdrop);

        function toggle() {
            var open = !document.body.classList.contains('ms-nav-open');
            apply(open);
            try {
                window.localStorage.setItem(storageKey, open ? '1' : '0');
            } catch (e) {
                return;
            }
        }

        backdrop.addEventListener('click', toggle);

        var buttons = document.querySelectorAll('[data-ms-sidebar-toggle]');
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].addEventListener('click', toggle);
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
