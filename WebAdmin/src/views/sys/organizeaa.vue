<template>
	<section>
		<el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
			<el-form :inline="true" :model="filters">
				<el-form-item label="部门名称">
					<el-input v-model="filters.key" clearable placeholder="姓名"></el-input>
				</el-form-item>
				<el-form-item label="状态">
					<el-select v-model="filters.status" clearable placeholder="部门状态">
						<el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"></el-option>
					</el-select>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" v-on:click="searchs" icon="el-icon-search">搜索</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="addDialog" icon="el-icon-plus">添加</el-button>
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
				<el-table-column prop="name" label="部门名称"></el-table-column>
				<el-table-column prop="leaderUser" label="部门负责人" width="180"></el-table-column>
				<el-table-column prop="sort" label="排序" width="180"></el-table-column>
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
							@click="modifyDialog(scope.row)"
							type="primary"
						>修改</el-link>
						<el-link
							icon="el-icon-delete"
							:underline="false"
							@click="deletes(scope.row)"
							type="danger"
							style="margin-left:15px;"
						>删除</el-link>
					</template>
				</el-table-column>
			</el-table>
		</template>

		<el-dialog :title="dialog.title" :visible.sync="dialogVisible" @close="onClose" width="850px">
			<el-row :gutter="15">
				<el-form ref="elForm" :model="formData" :rules="rules" label-width="100px">
					<el-col :span="24">
						<el-form-item label="所属部门" prop="parent">
							<el-cascader
								v-model="formData.parent"
								:options="parentIdOptions"
								:props="parentIdProps"
								:style="{width: '100%'}"
								placeholder="请选择所属部门"
								clearable
								filterable
							></el-cascader>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="部门名称" prop="name">
							<el-input v-model="formData.name" placeholder="请输入部门名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="部门负责人" prop="leaderUser">
							<el-input v-model="formData.leaderUser" placeholder="请输入部门负责人" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="联系电话" prop="leaderMobile">
							<el-input v-model="formData.leaderMobile" placeholder="请输入联系电话" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="联系邮箱" prop="leaderEmail">
							<el-input v-model="formData.leaderEmail" placeholder="请输入联系邮箱" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="状态" prop="status" required>
							<el-switch v-model="formData.status"></el-switch>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="排序" prop="sort" required>
							<el-slider :max="100" :step="1" v-model="formData.sort"></el-slider>
						</el-form-item>
					</el-col>
				</el-form>
			</el-row>
			<span slot="footer" class="dialog-footer">
				<el-button @click="close">取 消</el-button>
				<el-button type="primary" @click="handelConfirm" :loading="btnLoading">确 定</el-button>
			</span>
		</el-dialog>
	</section>
</template>

<script>
export default {
	data() {
		return {
			dialog: {
				title: '添加部门'
			},
			filters: {
				key: '',
				status: ''
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
			formData: {
				id: '0',
				parent: [],
				name: '',
				leaderUser: '',
				leaderMobile: '',
				leaderEmail: '',
				status: true,
				sort: 1
			},
			rules: {
				parent: [
					{
						required: true,
						type: 'array',
						message: '请至少选择一个所属部门',
						trigger: 'change'
					}
				],
				name: [
					{
						required: true,
						message: '请输入部门名称',
						trigger: 'blur'
					}
				],
				leaderUser: [
					{
						required: true,
						message: '请输入部门负责人',
						trigger: 'blur'
					}
				],
				leaderMobile: [
					{
						required: true,
						message: '请输入联系电话',
						trigger: 'blur'
					}
				],
				leaderEmail: []
			},
			parentIdOptions: [],
			parentIdProps: {
				multiple: false,
				checkStrictly: true,
				expandTrigger: 'hover'
			},
			tableData: [],
			dialogVisible: false,
			btnLoading: false
		}
	},
	mounted() {
		this.init()
	},
	methods: {
		init() {
			var _this = this
			this.$api
				.get('sysorganize')
				.then(res => {
					this.tableData = res.data.data
					this.loading = false
				})
				.catch(error => {
					console.warn(error)
				})
			this.$api
				.get('sysorganize/select')
				.then(res => {
					this.parentIdOptions = res.data.data
				})
				.catch(error => {
					console.warn(error)
				})
		},
		onClose() {
			this.formData = {
				id: '0',
				parent: [],
				name: '',
				leaderUser: '',
				leaderMobile: '',
				leaderEmail: '',
				status: true,
				sort: 1
			}
			this.$refs['elForm'].resetFields()
		},
		close() {
			this.dialogVisible = false
			this.$emit('update:visible', false)
		},
		handelConfirm() {
			this.$refs['elForm'].validate(valid => {
				if (!valid) return
				console.log('Add', this.formData)
				if (this.formData.id == '0') {
					this.add()
				} else {
					this.modify()
				}
			})
		},
		getParentIdOptions() {
			// TODO 发起请求获取数据
			this.parentIdOptions
		},
		searchs: function() {
			console.log(this.filters)
			if (this.filters.status == '') {
				this.filters.status = 0
			}
			this.loading = true
			this.$api
				.get('sysorganize', { params: this.filters })
				.then(res => {
					this.tableData = res.data.data
					this.loading = false
				})
				.catch(error => {
					console.warn(error)
				})
		},
		addDialog: function() {
			this.formData.id = '0'
			this.dialog.title = '添加部门'
			this.dialogVisible = true
		},
		modifyDialog: function(m) {
			this.formData = this.deepClone(m)
			this.formData.parent = []
			var _this = this
			if (m.parentIdList.indexOf(',') > -1) {
				var str = m.parentIdList.split(',')
				str.forEach(function(item, i) {
					if (item != m.id) {
						_this.formData.parent.push(item)
					}
				})
			} else {
				this.formData.parent.push(m.parentIdList)
			}
			this.dialog.title = '编辑部门'
			this.dialogVisible = true
		},
		add: function() {
			this.btnLoading = true
			console.log('Add', this.formData)
			this.$api
				.post('sysorganize', JSON.stringify(this.formData))
				.then(res => {
					this.btnLoading = false
					if (res.data.statusCode == 200) {
						this.init()
						this.close()
						this.$notify({ message: '添加成功', type: 'success' })
					} else {
						this.$notify({
							message: res.data.message,
							type: 'error'
						})
					}
				})
				.catch(error => {
					console.warn(error)
					this.close()
				})
		},
		modify: function() {
			this.btnLoading = true
			console.log(JSON.stringify(this.formData))
			this.$api
				.put('sysorganize', JSON.stringify(this.formData))
				.then(res => {
					this.btnLoading = false
					if (res.data.statusCode == 200) {
						this.init()
						this.close()
						this.$notify({ message: '添加成功', type: 'success' })
					} else {
						this.$notify({
							message: res.data.message,
							type: 'error'
						})
					}
				})
				.catch(error => {
					console.warn(error)
					this.close()
				})
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
				this.$api
					.delete('sysorganize', { data: [m.id] })
					.then(res => {
						if (res.data.statusCode == 200) {
							_this.init()
							this.$notify({
								message: '删除成功',
								type: 'success'
							})
						} else {
							this.$notify({
								message: res.data.message,
								type: 'error'
							})
						}
					})
					.catch(error => {
						console.warn(error)
					})
			})
		},
		deepClone(obj) {
			let newObj = obj.push ? [] : {}
			for (let attr in obj) {
				if (typeof obj[attr] === 'object') {
					newObj[attr] = this.deepClone(obj[attr])
				} else {
					newObj[attr] = obj[attr]
				}
			}
			return newObj
		}
	}
}
</script>