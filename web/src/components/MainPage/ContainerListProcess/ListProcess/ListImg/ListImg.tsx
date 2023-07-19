import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ListProcessStyles/ListProcess.module.scss'

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
		</>
	)
}

export default ListImg
