import { FC } from 'react'
import styles from './InfoFieldImg.module.scss'

interface InfoFieldImgProps {
	status: string
}

const InfoFieldImg: FC<InfoFieldImgProps> = ({ status }) => {
	return (
		<>
			{status === 'Согласовано' && (
				<img src='/completed.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'Не начат' && (
				<img src='/circle.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'Согласовано-Блокировано' && (
				<img src='/lock.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'Принят на проверку' && (
				<img
					src='/arrow-circle-down.svg'
					loading='lazy'
					className={styles.iconImg}
				/>
			)}
			{status === 'Отправлен на проверку' && (
				<img
					src='/arrow-circle-right.svg'
					loading='lazy'
					className={styles.iconImg}
				/>
			)}
			{status === 'В доработке' && (
				<img src='/rejected.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'Остановлен' && (
				<img
					src='/pause-circle.svg'
					loading='lazy'
					className={styles.iconImg}
				/>
			)}
			{status === 'в процессе' && (
				<img src='/inprogress.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'в доработке' && (
				<img src='/rejected.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'завершен' && (
				<img src='/completed.svg' loading='lazy' className={styles.iconImg} />
			)}
			{status === 'остановлен' && (
				<img
					src='/pause-circle.svg'
					loading='lazy'
					className={styles.iconImg}
				/>
			)}
			{status === 'согласован с замечаниями' && (
				<img src='/completed.svg' loading='lazy' className={styles.iconImg} />
			)}
		</>
	)
}

export default InfoFieldImg
