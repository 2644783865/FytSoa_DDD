<template>
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
</template>
<script>
export default {
	data() {
		return {
			dialog: {
				title: '添加部门'
			},
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
			dialogVisible: false,
			btnLoading: false
		}
    },
	methods: {
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
		refreshSelect(){
			console.log('refreshSelect');
		},
		handleAdd() {
			this.formData.id = '0'
			this.dialog.title = '添加部门'
			this.dialogVisible = true
		},
		handelModify(record) {
			console.log('BBB')
			this.dialog.title = '编辑部门'
			this.dialogVisible = true
			this.$nextTick(() => {
                this.formData = this.deepClone(record)
			})
		},
		handelConfirm() {
			this.$refs['elForm'].validate(valid => {
				if (!valid) return
				console.log('Add', this.formData)
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