import { Box, Button, TextField, Typography } from '@mui/material'
import FullLogo from '../assets/fulllogo.svg'
import { Controller, SubmitHandler, useForm } from 'react-hook-form'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'
import { useMutation } from '@tanstack/react-query'
import { loginApi } from '../api/loginApi'
import { useNavigate } from 'react-router-dom'
import styles from '../styles/LoginPageStyles/LoginPage.module.scss'

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
		if (mutation.data?.id) {
			localStorage.setItem('UserData', JSON.stringify(mutation.data))
			navigation('/')
		}
	}

	return (
		<Box component='main' className={styles.loginPage}>
			<img className={styles.logoImg} src={FullLogo} />
			<Box className={styles.loginBox}>
				<Box className={styles.loginContainer}>
					<Typography component='h1' className={styles.title}>
						Войти
					</Typography>
					<form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
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
											fontSize: { xl: '24px' },
											color: '#949494',
										},
									}}
									InputProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontWeight: '500px',
											fontSize: { xl: '24px' },
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
											fontSize: { xl: '24px' },
											color: '#949494',
										},
									}}
									InputProps={{
										sx: {
											fontFamily: 'Montserrat',
											fontWeight: '500px',
											fontSize: { xl: '24px' },
											borderRadius: '13px',
										},
									}}
								/>
							)}
						/>
						<Button variant='contained' type='submit' className={styles.btn}>
							продолжить
						</Button>
					</form>
				</Box>
			</Box>
		</Box>
	)
}

export default LoginPage
