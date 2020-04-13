import sys from './sys'

const api = {
    vm: {},
    install(Vue, options) {
        if (this.installed) {
            return
        }
        this.installed = true

        const api = {
            sys
        }

        Vue.api = api
        Object.defineProperty(Vue.prototype, '$api', {
            get() {
                return api
            }
        })
    }
}

export default api;