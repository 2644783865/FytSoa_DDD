import request from '@/api/request'
export default {
    getList: (params) => request.get('syspost', params),
    submit: (params) => request.post('syspost', params),
    delete: (params) => request.delete('syspost', params),
    modify: (params) => request.put('syspost', params)
}