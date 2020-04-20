<template>
	<section>
		<el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
			<el-form :inline="true" :model="filters">
				<el-form-item label="角色名称">
					<el-input v-model="filters.key" clearable placeholder="角色名称"></el-input>
				</el-form-item>
				<el-form-item label="状态">
					<el-select v-model="filters.status" clearable placeholder="角色状态">
						<el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"></el-option>
					</el-select>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" v-on:click="searchs" icon="el-icon-search">搜索</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="success" icon="el-icon-plus" @click="$refs.group.handleAddGroup()">添加角色组</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" icon="el-icon-plus" @click="$refs.modify.handleAdd()">添加角色</el-button>
				</el-form-item>
			</el-form>
		</el-col>
		<template>
			<el-table
				:data="tableData.items"
				style="width: 100%;margin-bottom: 20px;"
				row-key="id"
				v-loading="loading"
				border
				default-expand-all
			>
				<el-table-column type="index" width="80" label="序号"></el-table-column>
				<el-table-column prop="name" label="角色名称">
					<template slot-scope="scope">
						<i v-if="scope.row.parentId!='0'" class="el-icon-minus"></i>
						{{scope.row.name}}
						<el-link v-if="scope.row.parentId=='0'" :underline="false" class="cur-tag bg-cyan" type="primary">组</el-link>
					</template>
				</el-table-column>
				<el-table-column prop="sort" label="排序" width="180"></el-table-column>
				<el-table-column prop="isSystem" label="是否超管" width="180">
					<template slot-scope="scope">
						<el-link
							:underline="false"
							:type="scope.row.isSystem ? 'primary' : 'warning'"
						>{{scope.row.isSystem?'是':'否'}}</el-link>
					</template>
				</el-table-column>
				<el-table-column prop="status" label="状态" width="180">
					<template slot-scope="scope">
						<el-link
							:underline="false"
							:type="scope.row.status ? 'primary' : 'warning'"
						>{{scope.row.status?'正常':'停用'}}</el-link>
					</template>
				</el-table-column>
				<el-table-column prop="createTime" label="创建时间" width="180"></el-table-column>
				<el-table-column fixed="right" label="操作" width="160">
					<template slot-scope="scope">
						<el-link
							icon="el-icon-edit"
							:underline="false"
							type="primary"
							v-if="scope.row.parentId!='0' && !scope.row.isSystem"
							@click="$refs.modify.handelModify(scope.row)"
						>修改</el-link>
						<el-link
							icon="el-icon-edit"
							:underline="false"
							type="primary"
							v-if="scope.row.parentId=='0' && !scope.row.isSystem"
							@click="$refs.group.handelModifyGroup(scope.row)"
						>修改</el-link>
						<el-link
							icon="el-icon-delete"
							:underline="false"
							type="danger"
							@click="deletes(scope.row)"
							v-if="!scope.row.isSystem"
							style="margin-left:15px;"
						>删除</el-link>
					</template>
				</el-table-column>
			</el-table>
			<el-pagination
				background
				@size-change="handleSizeChange"
				@current-change="handleCurrentChange"
				:current-page="tableData.currentPage"
				:page-sizes="[10, 20, 50, 100,200,500,1000]"
				:page-size="10"
				layout="total, sizes, prev, pager, next, jumper"
				:total="tableData.totalItems"
			></el-pagination>
		</template>
		<modify ref="modify" @complete="onComplete"></modify>
		<group ref="group" @groupcomplete="onGroupComplete"></group>
	</section>
</template>

<script>
import modify from './modify'
import group from './group'
export default {
	components: {
		modify,
		group
	},
	data() {
		return {
			filters: {
				key: '',
				status: 1,
				limit: 10,
				page: 1
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
			tableData: {}
		}
	},
	mounted() {
		this.init()
		//this.$refs.modify.refreshSelect();
	},
	methods: {
		init: function() {
			this.$api.sys.role
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
			this.$confirm('确认删除选中记录吗', '提示', {
				type: 'warning'
			}).then(() => {
				this.loading = true
				this.$api.sys.role
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
		handleSizeChange(val) {
			this.filters.page = 1
			this.filters.limit = val
			this.init()
		},
		handleCurrentChange(val) {
			this.filters.page = val
			this.init()
		},
		onComplete() {
			this.init()
		},
		onGroupComplete() {
			this.init()
		}
	}
}
</script>