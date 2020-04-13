import request from '@/api/request'
export default {
    getList: (params) => request.get('sysorganize', params),
    submit: (params) => request.post('sysorganize', params),
    delete: (params) => request.delete('sysorganize', params),
    modify: (params) => request.put('sysorganize', params)
}