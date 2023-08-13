import { FC } from 'react'
import styles from './ListImg.module.scss'

interface ListImgProps {
	status: string
}

const ListImg: FC<ListImgProps> = ({ status }) => {
	return (
		<>
			{status === 'в процессе' && (
				<img src='/inprogress.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'завершен' && (
				<img src='/completed.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'остановлен' && (
				<img src='/pause-circle.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'отменен' && (
				<img src='/rejected.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'Согласовано' && (
				<img src='/completed.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Не начат' && (
				<img src='/stoppedProcess.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Согласовано-Блокировано' && (
				<img src='/lock.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Принят на проверку' && (
				<img
					src='/arrow-circle-down.svg'
					loading='lazy'
					className={styles.img}
				/>
			)}
			{status === 'Отправлен на проверку' && (
				<img
					src='/arrow-circle-right.svg'
					loading='lazy'
					className={styles.img}
				/>
			)}
			{status === 'Отменен' && (
				<img src='/rejected.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Остановлен' && (
				<img src='/pause-circle.svg' loading='lazy' className={styles.img} />
			)}
		</>
	)
}

export default ListImg
