// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', function (event) {
    if (event.request.mode === 'navigate') {
        event.respondWith(
            fetch(event.request)
                .catch(() => {
                    return caches.open('offline')
                        .then((cache) => {
                            return cache.match('index.html');
                        });
                })
        );
    }
});
/* Manifest version: PJJjdHr+ */
