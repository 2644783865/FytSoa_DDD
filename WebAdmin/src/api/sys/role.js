import request from '@/api/request'
export default {
    getList: (params) => request.get('sysrole', params),
    submit: (params) => request.post('sysrole', params),
    delete: (params) => request.delete('sysrole', params),
    modify: (params) => request.put('sysrole', params)
}