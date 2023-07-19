import { FC } from 'react'

interface HeaderFieldImgProps {
	status: string
}

const HeaderFieldImg: FC<HeaderFieldImgProps> = ({ status }) => {
	return (
		<>
			{status === 'Согласовано' && (
				<img src='/completed.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'Не начат' && (
				<img src='/circle.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'Согласовано-Блокировано' && (
				<img src='/lock.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'Принят на проверку' && (
				<img
					src='/arrow-circle-down.svg'
					loading='lazy'
					height='25px'
					width='25px'
				/>
			)}
			{status === 'Отправлен на проверку' && (
				<img
					src='/arrow-circle-right.svg'
					loading='lazy'
					height='25px'
					width='25px'
				/>
			)}
			{status === 'Отменен' && (
				<img src='/rejected.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'Остановлен' && (
				<img
					src='/pause-circle.svg'
					loading='lazy'
					height='25px'
					width='25px'
				/>
			)}
			{status === 'в процессе' && (
				<img src='/inprogress.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'отменен' && (
				<img src='/rejected.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'завершен' && (
				<img src='/completed.svg' loading='lazy' height='25px' width='25px' />
			)}
			{status === 'остановлен' && (
				<img
					src='/pause-circle.svg'
					loading='lazy'
					height='25px'
					width='25px'
				/>
			)}
			{status === 'согласован с замечаниями' && (
				<img src='/completed.svg' loading='lazy' height='25px' width='25px' />
			)}
		</>
	)
}

export default HeaderFieldImg
