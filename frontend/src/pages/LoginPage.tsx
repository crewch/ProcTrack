import { Box, Button, TextField, Typography } from '@mui/material'
import FullLogo from '../assets/fulllogo.svg'
import { Controller, SubmitHandler, useForm } from 'react-hook-form'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'
import { useMutation } from '@tanstack/react-query'
import { loginApi } from '../api/loginApi'
import { useNavigate } from 'react-router-dom'

const LoginPage = () => {
	const { control, handleSubmit, reset } = useForm({
		defaultValues: {
			email: '',
			password: '',
		},
	})

	const mutation = useMutation({
		mutationFn: loginApi.login,
	})

	const onSubmit: SubmitHandler<IFormInput> = userData => {
		mutation.mutate(userData)
		reset()
	}

	const navigation = useNavigate()
	if (mutation.isSuccess) {
		console.log(mutation.data)
		if (mutation.data?.token) {
			localStorage.setItem('TOKEN', mutation.data?.token)
			navigation('/')
		}
	}

	return (
		<Box
			component='main'
			sx={{
				backgroundColor: '#FFFFFF',
				height: '100%',
				p: 4,
			}}
		>
			<img style={{ position: 'absolute' }} src={FullLogo} />
			<Box
				sx={{
					height: '100%',
					display: 'flex',
					justifyContent: 'center',
					alignItems: 'center',
				}}
			>
				<Box
					sx={{
						width: '690px',
						height: '608px',
						borderRadius: '31px',
						boxShadow: '0px 0px 13px -2px rgba(0, 0, 0, 0.10)',
						display: 'flex',
						flexDirection: 'column',
						justifyContent: 'center',
						alignItems: 'center',
						gap: '55px',
					}}
				>
					<Typography
						component='h1'
						sx={{
							fontFamily: 'Montserrat',
							fontWeight: '600',
							fontSize: '64px',
						}}
					>
						Войти
					</Typography>
					<form
						onSubmit={handleSubmit(onSubmit)}
						style={{
							display: 'flex',
							flexDirection: 'column',
							gap: '28px',
						}}
					>
						<Controller
							name='email'
							control={control}
							rules={{ required: true }}
							render={({ field }) => (
								<TextField
									{...field}
									label='Логин'
									autoComplete='off'
									InputLabelProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontWeight: '500px',
											fontSize: '24px',
											color: '#949494',
										},
									}}
									InputProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontWeight: '500px',
											fontSize: '24px',
											borderRadius: '13px',
										},
									}}
								/>
							)}
						/>
						<Controller
							name='password'
							control={control}
							rules={{ required: true }}
							render={({ field }) => (
								<TextField
									{...field}
									type='password'
									label='Пароль'
									autoComplete='off'
									InputLabelProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontSize: '24px',
											color: '#949494',
										},
									}}
									InputProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontWeight: '500px',
											fontSize: '24px',
											borderRadius: '13px',
										},
									}}
								/>
							)}
						/>
						<Button
							variant='contained'
							type='submit'
							sx={{
								backgroundColor: '#5C98D0',
								width: '562px',
								height: '104px',
								borderRadius: '13px',
							}}
						>
							продолжить
						</Button>
					</form>
				</Box>
			</Box>
		</Box>
	)
}

export default LoginPage
