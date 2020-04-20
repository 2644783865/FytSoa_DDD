import Login from './views/Login.vue'
import NotFound from './views/404.vue'
import Home from './views/Home.vue'
import Main from './views/Main.vue'
import Table from './views/nav1/Table.vue'
import Form from './views/nav1/Form.vue'
import user from './views/nav1/user.vue'
import echarts from './views/charts/echarts.vue'

import sysorganize from './views/sys/organize'
import sysrole from './views/sys/role'
import syspost from './views/sys/post'
import sysadmin from './views/sys/admin'
import sysmenu from './views/sys/menu'
import syscode from './views/sys/code'
import syslog from './views/sys/logs'

let routes = [{
        path: '/login',
        component: Login,
        name: '',
        hidden: true
    },
    {
        path: '/404',
        component: NotFound,
        name: '',
        hidden: true
    },
    //{ path: '/main', component: Main },
    {
        path: '/',
        component: Home,
        name: '导航一',
        iconCls: 'el-icon-message', //图标样式class
        children: [
            { path: '/main', component: Main, name: '主页', hidden: true },
            { path: '/table', component: Table, name: 'Table' },
            { path: '/form', component: Form, name: 'Form' },
            { path: '/user', component: user, name: '列表' },
            { path: '/echarts', component: echarts, name: 'echarts' }
        ]
    },
    {
        path: '/',
        component: Home,
        name: '系统管理',
        iconCls: 'fa fa-bar-chart',
        children: [
            { path: '/sys/organize', component: sysorganize, name: '部门管理' },
            { path: '/sys/role', component: sysrole, name: '角色管理' },
            { path: '/sys/post', component: syspost, name: '岗位管理' },
            { path: '/sys/admin', component: sysadmin, name: '用户管理' },
            { path: '/sys/menu', component: sysmenu, name: '菜单管理' },
            { path: '/sys/code', component: syscode, name: '字典管理' },
            { path: '/sys/log', component: syslog, name: '日志管理' }
        ]
    },
    {
        path: '*',
        hidden: true,
        redirect: { path: '/404' }
    }
];

export default routes;