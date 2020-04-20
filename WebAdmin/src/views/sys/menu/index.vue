<template>
	<section>
		<el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
			<el-form :inline="true" :model="filters">
				<el-form-item label="菜单名称">
					<el-input v-model="filters.key" clearable placeholder="菜单名称"></el-input>
				</el-form-item>
				<el-form-item label="状态">
					<el-select v-model="filters.status" clearable placeholder="状态">
						<el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"></el-option>
					</el-select>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" v-on:click="searchs" icon="el-icon-search">搜索</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" icon="el-icon-plus" @click="$refs.modify.handleAdd()">添加</el-button>
				</el-form-item>
			</el-form>
		</el-col>
		<template>
			<el-table
				:data="tableData"
				style="width: 100%;margin-bottom: 20px;"
				row-key="id"
				v-loading="loading"
				border
				default-expand-all
				:tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
			>
				<el-table-column prop="name" label="菜单名称"></el-table-column>
				<el-table-column prop="code" label="权限标识" width="180"></el-table-column>
                <el-table-column prop="urls" label="路由" width="180"></el-table-column>
				<el-table-column prop="sort" label="排序" width="180"></el-table-column>
				<el-table-column prop="status" label="状态" width="180">
					<template slot-scope="scope">
						<el-link
							:underline="false"
							:type="scope.row.status ? 'primary' : 'warning'"
						>{{scope.row.status?'显示':'隐藏'}}</el-link>
					</template>
				</el-table-column>
				<el-table-column prop="createTime" label="创建时间" width="180"></el-table-column>
				<el-table-column fixed="right" label="操作" width="160">
					<template slot-scope="scope">
						<el-link
							icon="el-icon-edit"
							:underline="false"
							type="primary"
							@click="$refs.modify.handelModify(scope.row)"
						>修改</el-link>
						<el-link
							icon="el-icon-delete"
							:underline="false"
							type="danger"
							@click="deletes(scope.row)"
							style="margin-left:15px;"
						>删除</el-link>
					</template>
				</el-table-column>
			</el-table>
		</template>
		<modify ref="modify" @complete="onComplete"></modify>
	</section>
</template>

<script>
import modify from './modify'
export default {
	components: {
		modify
	},
	data() {
		return {
			filters: {
				key: '',
				status: 1
			},
			loading: true,
			options: [
				{
					value: 1,
					label: '正常'
				},
				{
					value: 2,
					label: '停用'
				}
			],
			tableData: []
		}
	},
	mounted() {
		this.init()
		//this.$refs.modify.refreshSelect();
	},
	methods: {
		init: function() {
			this.$api.sys.menu
				.getList(this.filters)
				.then(({ statusCode, data }) => {
					this.loading = false
					this.tableData = data
				})
				.catch(() => {
					this.loading = false
				})
		},
		searchs: function() {
			if (this.filters.status == '') {
				this.filters.status = 0
			}
			this.loading = true
			this.init()
		},
		deletes: function(m) {
			var _this = this
			this.$confirm(
				'确认删除选中记录吗？如果下面有子级，将子级一并删除！',
				'提示',
				{
					type: 'warning'
				}
			).then(() => {
				this.loading = true
				this.$api.sys.menu
					.delete([m.id])
					.then(({ statusCode, data, message }) => {
						if (statusCode == 200) {
							this.$notify({
								message: '删除成功',
								type: 'success'
							})
							this.init()
						} else {
							this.$notify({
								message: message,
								type: 'error'
							})
						}
					})
					.catch(() => {
						this.loading = false
					})
			})
		},
		onComplete() {
			this.init()
		}
	}
}
</script>